package com.example.filip.quizdown;


import android.util.Log;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Date;


public class Events {
    private static String TAG = Events.class.getSimpleName();

    private int id;
    private String name;
    private String description;
    private Date date;
    private String location;
    private String prize;
    private String rules;
    private String teamSize;


    public Events() {
    }

    public Events(int id, String teamSize, String rules, String prize, String location, Date date, String description, String name) {
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

    public String getDate(){
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        String d = format.format(date);
        return d;
    }

    public void setDate(String date) {
        SimpleDateFormat format = new SimpleDateFormat("yyyy-MM-dd");
        Date d = new Date();
        try {
            d = format.parse(date);
        }catch (ParseException e){
            Log.e(TAG, "ParseException: " + e.getMessage());
        }
        this.date = d;
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
