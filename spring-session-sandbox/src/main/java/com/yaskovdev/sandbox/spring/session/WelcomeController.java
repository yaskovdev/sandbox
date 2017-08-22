/*
 * Copyright 2012-2016 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

package com.yaskovdev.sandbox.spring.session;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.ModelAttribute;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.SessionAttributes;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpSession;
import java.util.Date;
import java.util.Enumeration;
import java.util.Map;

@Controller
@SessionAttributes("myRequestObject")
public class WelcomeController {

    @ModelAttribute("myRequestObject")
    public MyCommandBean addStuffToRequestScope() {
        System.out.println("Inside of addStuffToRequestScope");
        return new MyCommandBean("Hello World", 42);
    }

    @GetMapping("/")
    public String welcome(Map<String, Object> model) {
        model.put("time", new Date());
        model.put("message", "Some Message");
        return "welcome";
    }

    @RequestMapping("/foo")
    public String foo(Map<String, Object> model) {
        throw new RuntimeException("Foo");
    }

    @RequestMapping("/dosomething")
    public String requestHandlingMethod(Model model, HttpServletRequest request, HttpSession session) {
        System.out.println("Inside of dosomething handler method");

        System.out.println("--- Model data ---");
        Map modelMap = model.asMap();
        for (Object modelKey : modelMap.keySet()) {
            Object modelValue = modelMap.get(modelKey);
            System.out.println(modelKey + " -- " + modelValue);
        }

        System.out.println("=== Request data ===");
        Enumeration<String> reqEnum = request.getAttributeNames();
        while (reqEnum.hasMoreElements()) {
            String s = reqEnum.nextElement();
            System.out.println(s);
            System.out.println("==" + request.getAttribute(s));
        }

        System.out.println("*** Session data ***");
        Enumeration<String> e = session.getAttributeNames();
        while (e.hasMoreElements()) {
            String s = e.nextElement();
            System.out.println(s);
            System.out.println("**" + session.getAttribute(s));
        }

        return "summary";
    }
}
