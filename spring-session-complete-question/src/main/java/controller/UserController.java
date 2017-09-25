package controller;

import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.support.SessionStatus;

import static org.springframework.web.bind.annotation.RequestMethod.GET;
import static org.springframework.web.bind.annotation.RequestMethod.POST;

@Controller
public class UserController {

    @RequestMapping(method = GET, value = "/users")
    public String readUsers(ModelMap model) {
        model.addAttribute("users", "LIST_OF_USERS_FROM_SESSION");
        return "users";
    }

    @RequestMapping(method = POST, value = "/users")
    public void createUser() throws InterruptedException {
        Thread.sleep(10000);
    }

    @RequestMapping(method = POST, value = "/cancel")
    public void cancel(final SessionStatus sessionStatus) {
        sessionStatus.setComplete();
    }
}
