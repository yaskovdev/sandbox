package common;

import lombok.Data;

import model.Task;

@Data
public class Context {

	private String username;
	private String jwt;
	private Task task;
}
