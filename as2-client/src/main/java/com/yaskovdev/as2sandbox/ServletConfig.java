package com.yaskovdev.as2sandbox;

import static com.google.common.collect.ImmutableMap.of;

import javax.servlet.ServletContext;

import org.springframework.boot.web.servlet.ServletRegistrationBean;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;

import com.helger.as2servlet.AS2ReceiveServlet;
import com.helger.as2servlet.AS2WebAppListener;

@Configuration
public class ServletConfig {

	private final ServletContext servletContext;

	ServletConfig(final ServletContext servletContext) {
		this.servletContext = servletContext;
	}

	@Bean
	public ServletRegistrationBean<AS2ReceiveServlet> servletRegistrationBean() {
		AS2WebAppListener.staticInit(servletContext);
		final AS2ReceiveServlet servlet = new AS2ReceiveServlet();
		final ServletRegistrationBean<AS2ReceiveServlet> bean = new ServletRegistrationBean<>(servlet, "/as2");
		bean.setInitParameters(of("as2-servlet-config-filename", "config/config.xml"));
		return bean;
	}
}
