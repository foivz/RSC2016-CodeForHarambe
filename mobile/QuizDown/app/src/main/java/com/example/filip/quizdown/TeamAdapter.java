package com.example.filip.quizdown;


import android.content.Intent;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;


import java.util.List;

public class TeamAdapter extends RecyclerView.Adapter<TeamAdapter.TeamViewHolder>{

    private List<Team> teamList;

    public class TeamViewHolder extends RecyclerView.ViewHolder{
        public TextView tvName;
        public TextView tvTeamId;

        public TeamViewHolder(final View itemView) {
            super(itemView);
            tvName = (TextView) itemView.findViewById(R.id.tvName);
            tvTeamId = (TextView) itemView.findViewById(R.id.tvTeamID);

            Button btJoin = (Button) itemView.findViewById(R.id.btJoin);

            btJoin.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    String idUser = new ApiHandler().getUserIdByToken("http://rsc-harambe.azurewebsites.net/api/usertoken",LogInActivity.uid);
                    new ApiHandler().insertUserToTeam("http://rsc-harambe.azurewebsites.net/api/userteams", idUser, tvTeamId.getText().toString());
                    Intent i = new Intent(itemView.getContext(), QuizActivity.class);
                    itemView.getContext().startActivity(i);
                }
            });
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
        holder.tvTeamId.setText(team.getId());
    }

    @Override
    public int getItemCount() {
        return teamList.size();
    }
}
