package com.yaskovdev.as2sandbox;

import static com.google.common.collect.ImmutableMap.of;

import javax.servlet.ServletContext;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.web.servlet.ServletRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.helger.as2servlet.AS2ReceiveServlet;
import com.helger.as2servlet.AS2WebAppListener;

@Configuration
public class ServletConfig {

	@Autowired
	private ServletContext servletContext;

	@Bean
	public ServletRegistrationBean servletRegistrationBean() {
		AS2WebAppListener.staticInit(servletContext);
		final AS2ReceiveServlet servlet = new AS2ReceiveServlet();
		final ServletRegistrationBean<AS2ReceiveServlet> bean = new ServletRegistrationBean<>(servlet, "/as2");
		bean.setInitParameters(of("as2-servlet-config-filename", "config/config.xml"));
		return bean;
	}
}
