package com.yaskovdev.sandbox.distributedtransactionsandbox.client;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

@Component
public class JdbcClient {

    private final JdbcTemplate jdbcTemplate;

    @Autowired
    public JdbcClient(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public void createEvent() {
        jdbcTemplate.update("INSERT INTO events(type, name) VALUES (?, ?)", "Some Type", "Some Name");
    }
}
