package com.yaskovdev;

import org.springframework.format.annotation.DateTimeFormat;

import java.util.Date;

public class Form {

    @DateTimeFormat(pattern = "dd.MM.yyyy")
    private Date insuranceStartDate;

    public Date getInsuranceStartDate() {
        return insuranceStartDate;
    }

    public void setInsuranceStartDate(Date insuranceStartDate) {
        this.insuranceStartDate = insuranceStartDate;
    }

    @Override
    public String toString() {
        return "Form{" +
                "insuranceStartDate=" + insuranceStartDate +
                '}';
    }
}
