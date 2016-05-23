namespace wsy
{
    public partial class bit

    {
        #region Bit

        /// <summary>
        ///     取整数的某一位
        /// </summary>
        /// <param name="_Resource">要取某一位的整数</param>
        /// <param name="_Mask">要取的位置索引，自右至左为0-7</param>
        /// <returns>返回某一位的值（0或者1）</returns>
        public static int getIntegerSomeBit(int _Resource, int _Mask)
        {
            return _Resource >> _Mask & 1;
        }


        /// <summary>
        ///     将整数的某位置为0或1
        /// </summary>
        /// <param name="_Mask">整数的某位</param>
        /// <param name="a">整数</param>
        /// <param name="flag">是否置1，TURE表示置1，FALSE表示置0</param>
        /// <returns>返回修改过的值</returns>
        public static int setIntegerSomeBit(int _Mask, int a, bool flag)
        {
            if (flag)
            {
                a |= 0x1 << _Mask;
            }
            else
            {
                a &= ~(0x1 << _Mask);
            }
            return a;
        }

        #endregion

    }

}
