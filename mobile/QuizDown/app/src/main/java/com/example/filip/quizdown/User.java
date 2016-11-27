package com.example.filip.quizdown;



public class User {
    private String id;
    private String name;
    private String token;

    public User() {
    }

    public User(String name, String id, String token) {
        this.name = name;
        this.id = id;
        this.token = token;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getToken() {
        return token;
    }

    public void setToken(String token) {
        this.token = token;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }
}
