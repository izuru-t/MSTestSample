using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestStudy;
using System;
using System.Diagnostics;

namespace UnitTestProject
{
    /// <summary>
    /// UnitTest の概要の説明
    /// </summary>
    [TestClass]
    public class UnitTest
    {
        #region 自動生成されるコード
        public UnitTest()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        #endregion

        /// <summary>
        /// 私が良く使っているテストデータをCSVで受け取る方法
        /// 正常系と異常系を一緒にテストしているので少しコードが多い
        /// テストコードは如何に条件分岐やループをしないでシンプルに保てるかが重要
        /// </summary>
        [TestMethod]
        [DataSource(@"Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    @"|DataDirectory|\TestData.csv",
                    @"TestData#csv",
                    DataAccessMethod.Sequential)]
        public void GetOutTaxAmountTest()
        {
            #region テストデータ取得
            string no = TestContext.DataRow["No"].ToString();
            decimal amount;
            decimal taxRate;
            decimal actualTaxAmount;
            string actualException = TestContext.DataRow["Exception"].ToString();
            string actualMessage = TestContext.DataRow["ExceptionMessage"].ToString();
            // テストデータの型が正しくない場合はテスト失敗
            if (!decimal.TryParse(TestContext.DataRow["Amount"].ToString(), out amount))
                Assert.Fail("amountが数値ではありません"); 
            if (!decimal.TryParse(TestContext.DataRow["TaxRate"].ToString(), out taxRate))
                Assert.Fail("taxRateが数値ではありません"); 
            if (!decimal.TryParse(TestContext.DataRow["TaxAmount"].ToString(), out actualTaxAmount))
                Assert.Fail("actualTaxAmountが数値ではありません");
            #endregion
            try
            {
                // メソッドの実行結果を取得する
                var expected = TestTargetClass.GetOutTaxAmount(amount, taxRate);
                // 実行結果と期待値を比較する
                Assert.AreEqual(expected, actualTaxAmount);

                // 結果をDebug出力する
                var msg = string.Format("{0},{1},{2},{3},{4}"
                                       , no
                                       , "TaxAmount"
                                       , actualTaxAmount
                                       , expected
                                       , actualTaxAmount == expected
                                       );
                Debug.WriteLine("No,項目名,期待値,実行結果,期待値と実行結果の比較");
                Debug.WriteLine(msg);
            }
            catch (Exception ex)
            {
                // 例外が発生した場合
                // Exceptionの型が想定通りか
                Assert.IsTrue(ex.GetType().Name == actualException);
                // Exceptionのメッセージ内容が想定通りか（改行を除去して比較）
                Assert.AreEqual(ex.Message.Replace("\r\n", ""), actualMessage);

                // 結果をDebug出力する
                Debug.WriteLine("No,項目名,期待値,実行結果,期待値と実行結果の比較");
                var msg = string.Format("{0},{1},{2},{3},{4}"
                                       , no
                                       , "Exception"
                                       , actualException
                                       , ex.GetType().Name
                                       , actualException == ex.GetType().Name
                                       );
                Debug.WriteLine(msg);
                msg = string.Format("{0},{1},{2},{3},{4}"
                                       , no
                                       , "Message"
                                       , actualMessage
                                       , ex.Message.Replace("\r\n", "")
                                       , actualMessage == ex.Message.Replace("\r\n", "")
                                       );
                Debug.WriteLine(msg);
            }
        }

        /// <summary>
        /// 上記のテストコードからテストデータ取得部分と
        /// Debug出力の部分をメソッドに切り出したもの
        /// </summary>
        [TestMethod]
        [DataSource(@"Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    @"|DataDirectory|\TestData.csv",
                    @"TestData#csv",
                    DataAccessMethod.Sequential)]
        public void GetOutTaxAmountTest2()
        {
            var context = getTestData();
            try
            {
                // メソッドの実行結果を取得する
                var expected = TestTargetClass.GetOutTaxAmount(context.amount, context.taxRate);
                // 実行結果と期待値を比較する
                Assert.AreEqual(expected, context.actualTaxAmount);

                // 結果をDebug出力する
                writeExecLog(context.no, "TaxAmount", context.actualTaxAmount, expected, true);
            }
            catch (Exception ex)
            {
                // 例外が発生した場合
                // Exceptionの型が想定通りか
                Assert.IsTrue(ex.GetType().Name == context.actualException);
                // Exceptionのメッセージ内容が想定通りか（改行を除去して比較）
                Assert.AreEqual(ex.Message.Replace("\r\n", ""), context.actualMessage);

                // 結果をDebug出力する
                writeExecLog(context.no, "Exception", context.actualException, ex.GetType().Name, true);
                writeExecLog(context.no, "Message", context.actualMessage, ex.Message.Replace("\r\n", ""), false);
            }
        }
        /// <summary>
        /// TextContextを型付にしたようなものを返す
        /// </summary>
        /// <returns></returns>
        private (string no,decimal amount, decimal taxRate, decimal actualTaxAmount, string actualException,string actualMessage) getTestData() 
        {
            string no = TestContext.DataRow["No"].ToString();
            decimal amount;
            decimal taxRate;
            decimal actualTaxAmount;
            string actualException = TestContext.DataRow["Exception"].ToString();
            string actualMessage = TestContext.DataRow["ExceptionMessage"].ToString();
            // テストデータの型が正しくない場合はテスト失敗
            if (!decimal.TryParse(TestContext.DataRow["Amount"].ToString(), out amount))
                Assert.Fail("amountが数値ではありません");
            if (!decimal.TryParse(TestContext.DataRow["TaxRate"].ToString(), out taxRate))
                Assert.Fail("taxRateが数値ではありません");
            if (!decimal.TryParse(TestContext.DataRow["TaxAmount"].ToString(), out actualTaxAmount))
                Assert.Fail("actualTaxAmountが数値ではありません");

            return (no, amount, taxRate, actualTaxAmount, actualException, actualMessage);
        }
        /// <summary>
        /// 引数で渡された情報をDebug出力する
        /// </summary>
        /// <param name="no">テストケースNo</param>
        /// <param name="itemName">テスト項目名</param>
        /// <param name="actual">期待値</param>
        /// <param name="expected">実行結果</param>
        /// <param name="hasHeader">ヘッダを出力するか</param>
        private void writeExecLog(string no, string itemName, object actual, object expected,bool hasHeader) 
        {
            if(hasHeader)
                Debug.WriteLine("No,項目名,期待値,実行結果,期待値と実行結果の比較");

            var msg = string.Format("{0},{1},{2},{3},{4}"
                                   , no
                                   , itemName
                                   , actual
                                   , expected
                                   , actual == expected
                                   );
            Debug.WriteLine(msg);
        }
    }
}
