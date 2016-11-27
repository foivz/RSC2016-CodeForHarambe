package com.example.filip.quizdown;


public class Team {

    private String id;
    private String name;
    private String eventId;

    public Team() {
    }

    public Team(String id, String name, String eventId) {
        this.id = id;
        this.name = name;
        this.eventId = eventId;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getEventId() {
        return eventId;
    }

    public void setEventId(String eventId) {
        this.eventId = eventId;
    }
}
