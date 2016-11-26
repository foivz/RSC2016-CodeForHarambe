package com.example.filip.quizdown;


import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

public class EventAdapter extends RecyclerView.Adapter<EventAdapter.MyViewHolder>{
    private List<Events> eventsList;

    public class MyViewHolder extends RecyclerView.ViewHolder{
        public TextView title, date, description;

        public  MyViewHolder(View view){
            super(view);
            title = (TextView) view.findViewById(R.id.eventTitle);
            description = (TextView) view.findViewById(R.id.eventDescription);
            date = (TextView) view.findViewById(R.id.eventDate);
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
    }

    @Override
    public int getItemCount() {
        return eventsList.size();
    }


}
