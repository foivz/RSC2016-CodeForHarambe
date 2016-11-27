package com.example.filip.quizdown;

import com.google.firebase.database.Exclude;

import java.util.HashMap;
import java.util.Map;

/**
 * Created by Tomisav on 27.11.2016..
 */
public class Post {
    String name;
    String text;

    public Post() {
        // Default constructor required for calls to DataSnapshot.getValue(Post.class)
    }

    public Post(String name, String text) {
        this.name = name;
        this.text = text;
    }
    public String getName(){
        return this.name;
    }
    public String getText(){
        return this.text;
    }
    public Post getPost(){
        return this;
    }
    @Exclude
    public Map<String, Object> toMap() {
        HashMap<String, Object> result = new HashMap<>();
        result.put("name", name);
        result.put("text", text);


        return result;
    }
}
