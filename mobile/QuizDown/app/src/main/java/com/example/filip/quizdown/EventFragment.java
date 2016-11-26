package com.example.filip.quizdown;

import android.support.v7.widget.DividerItemDecoration;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v7.widget.DefaultItemAnimator;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import java.util.ArrayList;
import java.util.List;


public class EventFragment extends Fragment {

    private List<Events> eventList = new ArrayList<>();
    private RecyclerView recyclerView;
    private EventAdapter mAdapter;

    public EventFragment() {

    }

    @Override
    public void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
    }

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        View rootView =  inflater.inflate(R.layout.fragment_event, container, false);

        recyclerView = (RecyclerView)rootView.findViewById(R.id.recycler_view);

        prepareData();


        mAdapter = new EventAdapter(eventList);

        RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(getActivity());
        DividerItemDecoration mDividerItemDecoration;
        mDividerItemDecoration = new DividerItemDecoration(recyclerView.getContext(),
                DividerItemDecoration.VERTICAL);
        recyclerView.addItemDecoration(mDividerItemDecoration);
        recyclerView.setLayoutManager(mLayoutManager);
        recyclerView.setItemAnimator(new DefaultItemAnimator());
        recyclerView.setAdapter(mAdapter);

        return rootView;
    }

    private void prepareData(){
        Events events = new Events(1, "asd", "asdasd", "sdadasd", "asdsa", "19.01.1993", "desc", "Title");
        eventList.add(events);
        Events events2 = new Events(2, "asd", "asdasd", "sdadasd", "asdsa", "19.02.1993", "desc", "Title");
        eventList.add(events2);
        Events events3 = new Events(3, "asd", "asdasd", "sdadasd", "aaaaaaaa", "19.02.1993", "desc", "Title");
        eventList.add(events3);
    }
}
