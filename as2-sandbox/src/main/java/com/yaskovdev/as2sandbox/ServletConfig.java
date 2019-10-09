package com.yaskovdev.as2sandbox;

import com.helger.as2servlet.AS2ReceiveServlet;
import com.helger.as2servlet.AS2WebAppListener;
import org.springframework.boot.web.servlet.ServletRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import static com.google.common.collect.ImmutableMap.of;

@Configuration
public class ServletConfig {

    @Bean
    public AS2WebAppListener as2WebAppListener() {
        return new AS2WebAppListener();
    }

    @Bean
    public ServletRegistrationBean servletRegistrationBean() {
        final AS2ReceiveServlet servlet = new AS2ReceiveServlet();
        AS2WebAppListener.staticInit(servlet.getServletContext());
        final ServletRegistrationBean<AS2ReceiveServlet> bean = new ServletRegistrationBean<>(servlet, "/as2");
        bean.setInitParameters(of("as2-servlet-config-filename", "config/config.xml"));
        return bean;
    }
}
