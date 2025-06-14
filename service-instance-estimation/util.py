def calculate_composition_end_time(composition_speed, session_start_time, session_end_time, composition_start_time):
    assert session_start_time <= session_end_time, f"Session start time cannot be bigger than session end time"
    assert session_start_time <= composition_start_time, f"Session start time cannot be bigger than composition start time"
    assert composition_speed >= 1, f"Composition speed should be bigger or equal to 1"
    session_duration = session_end_time - session_start_time

    producing_speed = 1  # real time speed is always 1
    closing_speed = composition_speed - producing_speed

    produced_at_composition_start_time = (min(composition_start_time, session_end_time) - session_start_time) * producing_speed

    # at this moment either the session is ended, or it is ongoing, but we caught up
    catchup_or_session_end_time = min(composition_start_time + produced_at_composition_start_time / closing_speed, session_end_time)

    composition_duration = catchup_or_session_end_time - composition_start_time
    composed_amount = composition_speed * composition_duration
    total_amount = session_duration * producing_speed
    remaining_amount = total_amount - composed_amount

    if session_end_time == catchup_or_session_end_time:
        return catchup_or_session_end_time + remaining_amount / composition_speed
    else:
        return catchup_or_session_end_time + remaining_amount / producing_speed
