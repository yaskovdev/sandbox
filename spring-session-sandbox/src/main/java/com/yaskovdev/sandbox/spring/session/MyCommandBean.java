package com.yaskovdev.sandbox.spring.session;

public class MyCommandBean {

    private String someString;
    private int someNumber;

    public String getSomeString() {
        return someString;
    }

    public void setSomeString(String someString) {
        this.someString = someString;
    }

    public int getSomeNumber() {
        return someNumber;
    }

    public void setSomeNumber(int someNumber) {
        this.someNumber = someNumber;
    }

    @Override
    public String toString() {
        return "MyCommandBean [someString=" + someString + ", someNumber="
                + someNumber + "]";
    }

    public MyCommandBean(String someString, int someNumber) {
        super();
        this.someString = someString;
        this.someNumber = someNumber;
    }

}
