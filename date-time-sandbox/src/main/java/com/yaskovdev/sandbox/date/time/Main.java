package com.yaskovdev.sandbox.date.time;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;

public class Main {

    public static void main(final String[] args) throws ParseException {
        final DateFormat format = new SimpleDateFormat("dd.MM.yyyy Z");
        final Date from = format.parse("30.06.2015 UTC");
        final Date until = format.parse("01.07.2015 UTC");
        System.out.println(until.getTime() - from.getTime());
    }
}
