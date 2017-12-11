package com.yaskovdev;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
public class SumController {

    @RequestMapping(value = "/", method = RequestMethod.GET)
    public String index() {
        return "index";
    }

    @RequestMapping(value = "/addNumber", method = RequestMethod.POST)
    @ResponseBody
    public String controllerMethod(@RequestBody DataRequest request) {
        Integer value1 = request.getValue1();
        Integer value2 = request.getValue2();
        System.out.println("values :" + value1 + " , " + value2);
        int result = value1 + value2;
        System.out.println(result);
        String res = Integer.toString(result);
        return res;
    }
}
