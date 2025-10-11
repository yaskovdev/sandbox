package org.example;

import org.junit.jupiter.api.Test;

import static org.hamcrest.MatcherAssert.assertThat;
import static org.hamcrest.Matchers.equalTo;

class GreetingServiceImplTest {

    @Test
    void shouldReturnGreeting() {
        GreetingServiceImpl instanceUnderTest = new GreetingServiceImpl();
        assertThat(instanceUnderTest.greeting(), equalTo("Hello, World!"));
    }
}
