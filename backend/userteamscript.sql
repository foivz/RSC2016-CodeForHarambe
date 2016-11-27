CREATE TABLE TeamAnswers(
	id int PRIMARY KEY NOT NULL IDENTITY(1,1),
	answerID int NOT NULL,
	teamID int NOT NULL,
	eventID int NOT NULL,
	points int NULL,
	answersText varchar(500) null,
	constraint fk_answers_ta foreign key (answerID) references Answers(id),
	constraint fk_teams_ta foreign key (teamID) references Teams(id),
	constraint fk_event_ta foreign key (eventID) references Events(id)
);