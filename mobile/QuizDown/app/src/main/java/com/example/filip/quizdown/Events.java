package com.example.filip.quizdown;


public class Events {

    private int id;
    private String name;
    private String description;
    private String date;
    private String location;
    private String prize;
    private String rules;
    private String teamSize;


    public Events() {
    }

    public Events(int id, String teamSize, String rules, String prize, String location, String date, String description, String name) {
        this.id = id;
        this.teamSize = teamSize;
        this.rules = rules;
        this.prize = prize;
        this.location = location;
        this.date = date;
        this.description = description;
        this.name = name;
    }

    public String getTeamSize() {
        return teamSize;
    }

    public void setTeamSize(String teamSize) {
        this.teamSize = teamSize;
    }

    public String getRules() {
        return rules;
    }

    public void setRules(String rules) {
        this.rules = rules;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getPrize() {
        return prize;
    }

    public void setPrize(String prize) {
        this.prize = prize;
    }

    public String getDate() {
        return date;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
}
