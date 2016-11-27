package com.example.filip.quizdown;


import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

public class MyViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener{

    public Button btInfo;
    public Button btParticipate;

    public MyViewHolder(View itemView) {
        super(itemView);

        btParticipate = (Button) itemView.findViewById(R.id.btParticipate);
        btInfo = (Button) itemView.findViewById(R.id.btInfo);

        btParticipate.setOnClickListener(this);
        btInfo.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        if (view.getId() == btParticipate.getId()){
            Toast.makeText(view.getContext(), "ITEM PRESSED = " + String.valueOf(getAdapterPosition()), Toast.LENGTH_SHORT).show();
        } else {
            Toast.makeText(view.getContext(), "ROW PRESSED = " + String.valueOf(getAdapterPosition()), Toast.LENGTH_SHORT).show();
        }
    }
}
