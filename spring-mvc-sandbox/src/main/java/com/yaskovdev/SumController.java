package com.yaskovdev;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import java.util.Date;

@Controller
public class SumController {

    @RequestMapping(value = "/", method = RequestMethod.GET)
    public String index(final Model model) {
        final Form form = new Form();
        form.setInsuranceStartDate(new Date());
        model.addAttribute("form", form);
        return "index";
    }

    @RequestMapping(value = "/insurances", method = RequestMethod.POST)
    public String controllerMethod(@ModelAttribute("form") final Form form) {
        System.out.println(form);
        return "thank-you";
    }
}
