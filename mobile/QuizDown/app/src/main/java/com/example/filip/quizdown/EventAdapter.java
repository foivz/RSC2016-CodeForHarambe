package com.example.filip.quizdown;


import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

public class EventAdapter extends RecyclerView.Adapter<EventAdapter.MyViewHolder>{
    private List<Events> eventsList;

    public static class MyViewHolder extends RecyclerView.ViewHolder{

        public Button btInfo;
        public Button btParticipate;

        public TextView title, date, description, id;

        public  MyViewHolder(final View view){
            super(view);
            title = (TextView) view.findViewById(R.id.eventTitle);
            description = (TextView) view.findViewById(R.id.eventDescription);
            date = (TextView) view.findViewById(R.id.eventDate);
            id = (TextView) view.findViewById(R.id.tvEventID);

            btInfo = (Button) view.findViewById(R.id.btInfo);
            btParticipate = (Button) view.findViewById(R.id.btParticipate);

            btInfo.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(view.getContext(), EventDetails.class);
                    TextView tvEventID = (TextView)view.findViewById(R.id.tvEventID);
                    String message = tvEventID.getText().toString();
                    intent.putExtra(Intent.EXTRA_TEXT, message);
                    view.getContext().startActivity(intent);
                }
            });
            btParticipate.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    Intent intent = new Intent(view.getContext(), TeamActivity.class);
                    TextView tvEventID = (TextView)view.findViewById(R.id.tvEventID);
                    String message = tvEventID.getText().toString();
                    intent.putExtra(Intent.EXTRA_TEXT, message);
                    view.getContext().startActivity(intent);
                }
            });
        }

    }

    public EventAdapter(List<Events> eventsList){
        this.eventsList = eventsList;
    }

    @Override
    public MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.event_list_row, parent, false);

        return new MyViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(MyViewHolder  holder, int position) {
        Events events = eventsList.get(position);
        holder.title.setText(events.getName());
        holder.description.setText(events.getDescription());
        holder.date.setText(events.getDate());
        holder.id.setText(events.getId());
    }

    @Override
    public int getItemCount() {
        return eventsList.size();
    }


}
