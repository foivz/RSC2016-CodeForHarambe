package com.example.filip.quizdown;



public class History {

    private String title;
    private String description;
    private String score;

    public History() {
    }

    public History(String score, String description, String title) {
        this.score = score;
        this.description = description;
        this.title = title;
    }

    public String getScore() {
        return score;
    }

    public void setScore(String score) {
        this.score = score;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }
}
