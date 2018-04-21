public class JustClass implements Calculator {

    @Override
    public int calculate() {
        return method1();
    }

    private static int method1() {
        return method2();
    }

    private static int method2() {
        return method3();
    }

    private static int method3() {
        return method4();
    }

    private static int method4() {
        return method5();
    }

    private static int method5() {
        return method6();
    }

    private static int method6() {
        return method7();
    }

    private static int method7() {
        return method8();
    }

    private static int method8() {
        return method9();
    }

    private static int method9() {
        return method10();
    }

    private static int method10() {
        return 2 + 2;
    }
}
