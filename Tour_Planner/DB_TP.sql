create table tour
(
    id               text not null
        constraint tour_pk
            primary key,
    title            text not null,
    description      text,
    _from            text,
    _to              text,
    transport_type   text,
    distance         text,
    estimated_time   text,
    route_image_path text
);

alter table tour
    owner to postgres;

create unique index tour_id_uindex
    on tour (id);

create table tour_log
(
    id         text not null
        constraint tour_log_pk
            primary key,
    date       text,
    time       integer,
    rating     integer,
    difficulty integer,
    total_time integer,
    comment    text,
    tour_id    text
        constraint tour_log_tour_id_fk
            references tour
            on delete cascade
);

alter table tour_log
    owner to postgres;

create unique index tour_log_id_uindex
    on tour_log (id);
