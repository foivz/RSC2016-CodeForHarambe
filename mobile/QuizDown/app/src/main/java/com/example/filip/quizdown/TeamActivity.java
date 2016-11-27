package com.example.filip.quizdown;

import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.design.widget.FloatingActionButton;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import java.util.ArrayList;
import java.util.List;
import java.util.logging.Handler;

public class TeamActivity extends AppCompatActivity {

    private List<Team> teamList = new ArrayList<>();
    private RecyclerView recyclerView;
    private TeamAdapter mAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_team);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);

        recyclerView = (RecyclerView)findViewById(R.id.listTeams);

        Intent intent = getIntent();
        final String message = intent.getStringExtra(Intent.EXTRA_TEXT);

        ApiHandler handler = new ApiHandler();
        teamList = handler.getTeamByEventId("http://rsc-harambe.azurewebsites.net/api/eventteams", message);

        mAdapter = new TeamAdapter(teamList);

        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this);
        DividerItemDecoration mDividerItemDecoration;
        mDividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),
                DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(mDividerItemDecoration);
        recyclerView.setLayoutManager(mLayoutManager);
        recyclerView.setItemAnimator(new DefaultItemAnimator());
        recyclerView.setAdapter(mAdapter);

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fabAdd);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                AlertDialog.Builder builder = new AlertDialog.Builder(TeamActivity.this);
                LayoutInflater inflater = TeamActivity.this.getLayoutInflater();
                builder.setView(inflater.inflate(R.layout.create_team, null));
                View dialogView = inflater.inflate(R.layout.create_team, null);
                builder.setView(dialogView);

                final AlertDialog alertDialog = builder.create();
                alertDialog.show();

                Button ok = (Button) alertDialog.findViewById(R.id.btOk);
                Button cancel = (Button) alertDialog.findViewById(R.id.btCancel);

                ok.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View vi) {
                        EditText et = (EditText)alertDialog.findViewById(R.id.etTeamName);
                        String name = et.getText().toString();
                        if(name.length() == 0) {
                            alertDialog.cancel();
                            return;
                        }
                        Team t = new Team();
                        t.setName(name);
                        t.setEventId(message);
                        String id = new ApiHandler().insertTeam("http://rsc-harambe.azurewebsites.net/api/teams", t);
                        String idUser = new ApiHandler().getUserIdByToken("http://rsc-harambe.azurewebsites.net/api/usertoken",LogInActivity.uid);
                        new ApiHandler().insertUserToTeam("http://rsc-harambe.azurewebsites.net/api/userteams",idUser, id);
                        Intent i = new Intent(getApplicationContext(), WaitResultActivity.class);
                        startActivity(i);
                    }
                });

                cancel.setOnClickListener(new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        alertDialog.cancel();
                    }
                });

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
