package com.yaskovdev.sandbox.spring.session;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.bind.annotation.SessionAttributes;
import org.springframework.web.bind.support.SessionStatus;

import java.util.Map;

@Controller
@SessionAttributes("message")
class NotificationController {

    @GetMapping("/")
    String index(final Map<String, Object> model) {
        model.put("message", "Some Message");
        return "index";
    }

    @GetMapping("/summary")
    String requestHandlingMethod() {
        return "summary";
    }

    @PostMapping("/notifications")
    @ResponseBody
    void performLongRunningOperation() throws InterruptedException {
        Thread.sleep(5000);
    }

    @PostMapping("/cancel")
    @ResponseBody
    void cancel(final SessionStatus sessionStatus) {
        sessionStatus.setComplete();
    }
}
