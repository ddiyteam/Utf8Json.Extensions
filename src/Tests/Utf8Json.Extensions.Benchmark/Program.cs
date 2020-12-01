using BenchmarkDotNet.Running;

namespace Utf8Json.Extensions.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<DeserializeEnumCaseIgnoreBenchmark>();
            BenchmarkRunner.Run<SerializeEnumCaseIgnoreBenchmark>();
            
            //TestRun();

            //Console.ReadLine();
        }

        private static void TestRun()
        {
            var testS = new SerializeEnumCaseIgnoreBenchmark();
            var q1 = testS.Utf8Json_EnumDefault();
            var q2 = testS.Utf8Json_EnumCaseIgnoreDefault();
            var q3 = testS.NewtonSoft_EnumDefault();

            var q4 = testS.Utf8Json_EnumCamelCase();
            var q5 = testS.Utf8Json_EnumCaseIgnoreCamelCase();
            var q6 = testS.NewtonSoft_EnumCamelCase();

            var q7 = testS.Utf8Json_EnumSnakeCase();
            var q8 = testS.Utf8Json_EnumCaseIgnoreSnakeCase();
            var q9 = testS.NewtonSoft_EnumSnakeCase();

            var q10 = testS.Utf8Json_EnumUnderlying();
            var q11 = testS.Utf8Json_EnumCaseIgnoreUnderlying();
            var q12 = testS.NewtonSoft_EnumUnderlying();


            var testD = new DeserializeEnumCaseIgnoreBenchmark();
            var d1 = testD.Utf8Json_EnumDefault();
            var d2 = testD.Utf8Json_EnumCaseIgnoreDefault();
            var d3 = testD.NewtonSoft_EnumDefault();

            var d4 = testD.Utf8Json_EnumCamelCase();
            var d5 = testD.Utf8Json_EnumCaseIgnoreCamelCase();
            var d6 = testD.NewtonSoft_EnumCamelCase();

            var d7 = testD.Utf8Json_EnumSnakeCase();
            var d8 = testD.Utf8Json_EnumCaseIgnoreSnakeCase();
            var d9 = testD.NewtonSoft_EnumSnakeCase();

            var d10 = testD.Utf8Json_EnumUnderlying();
            var d11 = testD.Utf8Json_EnumCaseIgnoreUnderlying();
            var d12 = testD.NewtonSoft_EnumUnderlying();

        }
    }
}
