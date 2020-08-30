using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestStudy;

namespace UnitTestProject
{
    /// <summary>
    /// 簡単に使うとこんな感じで
    /// 1メソッド1ケースで書いていく感じです
    /// </summary>
    [TestClass]
    public class SimpleUnitTest
    {
        /// <summary>
        /// 単純計算なのでこんな感じ
        /// </summary>
        [TestMethod]
        public void GetOutTaxAmountTest()
        {
            decimal amount = 1000;
            decimal taxRate = 0.10m;    // 現在の消費税率10%
            decimal actual = 1000 * 0.1m;

            // 最大値とか最小値とか境界値のテストをしたいから
            // ここでamountの値とか変えて何度も実行したらいいか！
            // ってやっちゃダメです。

            // メソッドの実行結果を取得する
            var expected = TestTargetClass.GetOutTaxAmount(amount, taxRate);

            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// 例外ケースのテスト（旧来の書き方）
        /// amountが負数の時
        /// </summary>
        /// <remarks>
        /// ExpectedExceptionっていう属性にExceptionの型を渡せば
        /// テスト実行時にそのExceptionが
        /// 　発生していれば成功
        /// 　発生していなければ失敗
        /// という結果になる
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetOutTaxAmount_ExceptionTest1()
        {
            decimal amount = -1000;    // 人にお金を渡して税金分返ってくるなんて事はないよ？
            decimal taxRate = 0.10m;
            decimal actual = 1000 * 0.1m;

            // メソッドの実行結果を取得する
            var expected = TestTargetClass.GetOutTaxAmount(amount, taxRate);

            Assert.AreEqual(expected, actual);
        }
        /// <summary>
        /// 例外ケースのテスト（VS2017からはこうやって書ける）
        /// 税率が99%を超える時
        /// </summary>
        /// <remarks>
        /// AssertにThrowsExceptionというメソッドが追加されている！！
        /// Exceptionの型を指定してFuncを引数にすれば上のExpectedExceptionと同じ意味になるよ
        /// </remarks>
        [TestMethod]
        public void GetOutTaxAmount_ExceptionTest2()
        {
            decimal amount = 1000;
            decimal taxRate = 10;    // 現在の消費税率10%だから10って渡す人がいるんだよね

            // メソッドの実行結果を取得する
            Assert.ThrowsException<ArgumentOutOfRangeException>(()=> TestTargetClass.GetOutTaxAmount(amount, taxRate));
        }
        /// <summary>
        /// 例外ケースのテスト
        /// 税率が99%を超える時
        /// 例外の種類と例外メッセージをテスト対象とする
        /// </summary>
        /// <remarks>
        /// 上でやったExpectedExceptionもThrowsExceptionも例外の型は見てくれるけど
        /// メッセージの内容は見てくれない
        /// そこで、try-catchで取得したExceptionのメッセージと比較する
        /// </remarks>
        [TestMethod]
        public void GetOutTaxAmount_ExceptionTest3()
        {
            decimal amount = 1000;
            decimal taxRate = 10;    // 現在の消費税率10%だから10って渡す人がいるんだよね
            string actualMessage = "税率は0.01～0.99の間で指定してください\r\nパラメーター名:taxRate";

            try
            {
                // メソッドの実行結果を取得する
                var expected = TestTargetClass.GetOutTaxAmount(amount, taxRate);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(ArgumentOutOfRangeException));
                Assert.AreEqual(ex.Message, actualMessage);
            }
        }
    }
}
