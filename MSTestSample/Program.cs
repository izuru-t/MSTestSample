using System;

namespace MSTestStudy
{
    /// <summary>
    /// VisualStduioに標準で実装されているUnitTest機能
    /// MSTestの使い方を忘れないようにメモしておく
    /// javaだとjUnitとか似たものがあるし、基本的な考え方は同じなので
    /// UnitTestを実装する癖をつけておくのが良いと思う
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
    /// <summary>
    /// テストしたいメソッドがあるクラス
    /// </summary>
    public static class TestTargetClass
    {
        /// <summary>
        /// 渡されたamountの外税額を計算する
        /// </summary>
        /// <param name="amount">金額</param>
        /// <param name="taxRate">税率</param>
        /// <returns>外税額</returns>
        /// <exception cref="ArgumentOutOfRangeException">金額は正数で指定してください</exception>
        /// <exception cref="ArgumentOutOfRangeException">税率は0.01～0.99の間で指定してください</exception>
        public static decimal GetOutTaxAmount(decimal amount, decimal taxRate) 
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("amount","金額は正数で指定してください");
            if (taxRate <= 0 || taxRate >= 1) throw new ArgumentOutOfRangeException("taxRate","税率は0.01～0.99の間で指定してください");
            return amount * taxRate;
        }
    }
}
