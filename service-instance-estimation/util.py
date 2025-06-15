def calculate_composition_end_time(composition_speed, session_start_time, session_end_time, composition_start_time):
    assert session_start_time <= session_end_time, f"Session cannot end before start"
    assert session_start_time <= composition_start_time, f"Composition cannot start before session"
    assert composition_speed >= 1, f"Composition speed cannot be smaller than 1"

    producing_speed = 1  # real time speed is always 1
    closing_speed = composition_speed - producing_speed

    produced_at_composition_start_time = (min(composition_start_time, session_end_time) - session_start_time) * producing_speed

    # at this moment either the session is ended, or it is ongoing, but we caught up
    catchup_or_session_end_time = session_end_time if closing_speed == 0 else min(composition_start_time + produced_at_composition_start_time / closing_speed, session_end_time)

    composition_duration = catchup_or_session_end_time - composition_start_time
    composed_amount = composition_speed * composition_duration
    total_amount = (session_end_time - session_start_time) * producing_speed
    remaining_amount = total_amount - composed_amount

    return catchup_or_session_end_time + remaining_amount / (composition_speed if catchup_or_session_end_time == session_end_time else producing_speed)
