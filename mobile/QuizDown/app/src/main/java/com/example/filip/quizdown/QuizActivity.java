package com.example.filip.quizdown;

import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.app.Activity;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.view.View;
import android.widget.TextView;
import android.support.v7.app.AppCompatActivity;

public class QuizActivity extends AppCompatActivity{

    private TextView answer1;
    private TextView answer2;
    private TextView answer3;
    private TextView answer4;

    @Override
    protected void onCreate(Bundle savedInstanceState){
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_quiz);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbarQuiz);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        answer1 = (TextView)findViewById(R.id.answer1);
        answer2 = (TextView)findViewById(R.id.answer2);
        answer3 = (TextView)findViewById(R.id.answer3);
        answer4 = (TextView)findViewById(R.id.answer4);

        answer1.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                view.setBackgroundColor(Color.parseColor("#1abc9c"));
                answer1.setTextColor(Color.parseColor("#ecf0f1"));
            }
        });

        answer2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                view.setBackgroundColor(Color.parseColor("#1abc9c"));
                answer2.setTextColor(Color.parseColor("#ecf0f1"));
            }
        });
        answer3.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                view.setBackgroundColor(Color.parseColor("#1abc9c"));
                answer3.setTextColor(Color.parseColor("#ecf0f1"));
            }
        });
        answer4.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                view.setBackgroundColor(Color.parseColor("#c0392b"));
                answer4.setTextColor(Color.parseColor("#ecf0f1"));
            }
        });
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Intent myIntent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivityForResult(myIntent, 0);
        return true;
    }
}