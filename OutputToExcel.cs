using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace wsy
{
    public partial class OutputToExcel

    {
        #region OutputToExcel

        public static bool OutputToxls(DataTable Table, string ExcelFilePath)
        {
            //if (File.Exists(ExcelFilePath))
            //{
            //    throw new Exception("该文件已经存在！");
            //}

            if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
            {
                Table.TableName = "Sheet1";
            }

            //数据表的列数
            var ColCount = Table.Columns.Count;

            //用于记数，实例化参数时的序号
            var i = 0;

            //创建参数
            var para = new OleDbParameter[ColCount];

            //创建表结构的SQL语句
            var TableStructStr = @"Create Table " + Table.TableName + "(";

            //连接字符串
            var connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath +
                             ";Extended Properties=Excel 8.0;";
            var objConn = new OleDbConnection(connString);

            //创建表结构
            var objCmd = new OleDbCommand();

            //数据类型集合
            var DataTypeList = new ArrayList();
            DataTypeList.Add("System.Decimal");
            DataTypeList.Add("System.Double");
            DataTypeList.Add("System.Int16");
            DataTypeList.Add("System.Int32");
            DataTypeList.Add("System.Int64");
            DataTypeList.Add("System.Single");

            //遍历数据表的所有列，用于创建表结构
            foreach (DataColumn col in Table.Columns)
            {
                //如果列属于数字列，则设置该列的数据类型为double
                if (DataTypeList.IndexOf(col.DataType.ToString()) >= 0)
                {
                    para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.Double);
                    objCmd.Parameters.Add(para[i]);

                    //如果是最后一列
                    if (i + 1 == ColCount)
                    {
                        TableStructStr += col.ColumnName + " double)";
                    }
                    else
                    {
                        TableStructStr += col.ColumnName + " double,";
                    }
                }
                else
                {
                    para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
                    objCmd.Parameters.Add(para[i]);

                    //如果是最后一列
                    if (i + 1 == ColCount)
                    {
                        TableStructStr += col.ColumnName + " varchar)";
                    }
                    else
                    {
                        TableStructStr += col.ColumnName + " varchar,";
                    }
                }
                i++;
            }

            //创建Excel文件及文件结构
            try
            {
                objCmd.Connection = objConn;
                objCmd.CommandText = TableStructStr;

                if (objConn.State == ConnectionState.Closed)
                {
                    objConn.Open();
                }
                objCmd.ExecuteNonQuery();
            }
            catch (Exception exp)
            {
                throw exp;
            }

            //插入记录的SQL语句
            var InsertSql_1 = "Insert into " + Table.TableName + " (";
            var InsertSql_2 = " Values (";
            var InsertSql = "";

            //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
            for (var colID = 0; colID < ColCount; colID++)
            {
                if (colID + 1 == ColCount) //最后一列
                {
                    InsertSql_1 += Table.Columns[colID].ColumnName + ")";
                    InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
                }
                else
                {
                    InsertSql_1 += Table.Columns[colID].ColumnName + ",";
                    InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
                }
            }

            InsertSql = InsertSql_1 + InsertSql_2;

            //遍历数据表的所有数据行
            for (var rowID = 0; rowID < Table.Rows.Count; rowID++)
            {
                for (var colID = 0; colID < ColCount; colID++)
                {
                    if (para[colID].DbType == DbType.Double && Table.Rows[rowID][colID].ToString().Trim() == "")
                    {
                        para[colID].Value = 0;
                    }
                    else
                    {
                        para[colID].Value = Table.Rows[rowID][colID].ToString().Trim();
                    }
                }
                try
                {
                    objCmd.CommandText = InsertSql;
                    objCmd.ExecuteNonQuery();
                }
                catch (Exception exp)
                {
                    var str = exp.Message;
                }
            }
            try
            {
                if (objConn.State == ConnectionState.Open)
                {
                    objConn.Close();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return true;
        }

        public static DataTable ConvertDataReaderToDataTable(OleDbDataReader reader)
        {
            var objDataTable = new DataTable("TmpDataTable");
            try
            {
                var intFieldCount = reader.FieldCount; //获取当前行中的列数；
                for (var intCounter = 0; intCounter <= intFieldCount - 1; intCounter++)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                }
                //populate   datatable   
                objDataTable.BeginLoadData();
                //object[]   objValues   =   new   object[intFieldCount   -1];   
                var objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                objDataTable.EndLoadData();
                return objDataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("转换出错出错!", ex);
            }
        }

        #endregion
    }

}
