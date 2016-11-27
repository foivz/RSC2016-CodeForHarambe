package com.example.filip.quizdown;


import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;


import java.util.List;

public class TeamAdapter extends RecyclerView.Adapter<TeamAdapter.TeamViewHolder>{

    private List<Team> teamList;

    public class TeamViewHolder extends RecyclerView.ViewHolder{
        public TextView tvName;

        public TeamViewHolder(View itemView) {
            super(itemView);
            tvName = (TextView) itemView.findViewById(R.id.tvName);
        }
    }

    public TeamAdapter(List<Team> teamList) {
        this.teamList = teamList;
    }

    @Override
    public TeamViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.team_list_row, parent, false);

        return new TeamAdapter.TeamViewHolder(itemView);
    }

    @Override
    public void onBindViewHolder(TeamViewHolder holder, int position) {
        Team team = teamList.get(position);
        holder.tvName.setText(team.getName());
    }

    @Override
    public int getItemCount() {
        return teamList.size();
    }
}
