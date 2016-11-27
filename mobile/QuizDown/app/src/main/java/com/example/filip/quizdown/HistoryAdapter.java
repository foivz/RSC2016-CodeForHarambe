package com.example.filip.quizdown;


import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import java.util.List;

public class HistoryAdapter extends RecyclerView.Adapter<HistoryAdapter.HistoryViewHolder> {

    private List<History> historyList;

    public class HistoryViewHolder extends RecyclerView.ViewHolder{
        public TextView title, score, description;

        public  HistoryViewHolder(View view){
            super(view);
            title = (TextView) view.findViewById(R.id.historyTitle);
            description = (TextView) view.findViewById(R.id.historyScore);
            score = (TextView) view.findViewById(R.id.historyScore);
        }
    }

    public HistoryAdapter(List<History> historyList) {
        this.historyList = historyList;
    }

    @Override
    public HistoryViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.history_list_row, parent, false);

        return new HistoryAdapter.HistoryViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(HistoryViewHolder holder, int position) {
        History history = historyList.get(position);
        holder.title.setText(history.getTitle());
        holder.description.setText(history.getDescription());
        holder.score.setText(history.getScore());
    }

    @Override
    public int getItemCount() {
        return historyList.size();
    }
}
