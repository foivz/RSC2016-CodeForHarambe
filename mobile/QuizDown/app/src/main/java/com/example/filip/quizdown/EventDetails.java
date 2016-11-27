package com.example.filip.quizdown;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.widget.TextView;

public class EventDetails extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_event_details);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        Intent intent = getIntent();
        String message = intent.getStringExtra(Intent.EXTRA_TEXT);
        Events event = new ApiHandler().getEventById("http://rsc-harambe.azurewebsites.net/api/qevents", message);

        TextView tvTitle = (TextView)findViewById(R.id.tvTitle);
        tvTitle.setText(event.getName());

        TextView tvDesc = (TextView)findViewById(R.id.tvDescription);
        tvDesc.setText(event.getDescription());

        TextView tvDate = (TextView)findViewById(R.id.tvDate);
        tvDate.setText(event.getDate());

        TextView tvLocation = (TextView)findViewById(R.id.tvLocation);
        tvLocation.setText(event.getLocation());

        TextView tvPrize = (TextView)findViewById(R.id.tvPrize);
        tvPrize.setText(event.getPrize());

        TextView tvRules = (TextView)findViewById(R.id.tvRules);
        tvRules.setText(event.getRules());

        TextView tvTeamSize = (TextView)findViewById(R.id.tvTeamSize);
        tvTeamSize.setText(event.getTeamSize());
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        Intent myIntent = new Intent(getApplicationContext(), MenuActivity.class);
        startActivityForResult(myIntent, 0);
        return true;
    }
}
