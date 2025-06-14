from datetime import timedelta


def calculate_composition_end_time(faster_than_real_time_coefficient, session_start_time, session_end_time, composition_start_time):
    session_duration = session_end_time - session_start_time

    # composition started after session ended
    if composition_start_time >= session_end_time:
        return composition_start_time + session_duration / faster_than_real_time_coefficient

    # composition started before session ended
    producing_speed = 1
    consuming_speed = producing_speed * faster_than_real_time_coefficient
    closing_speed = consuming_speed - producing_speed  # real time speed is always 1

    produced_at_composition_start_time = (composition_start_time - session_start_time) * producing_speed

    # at this moment either the session is ended, or it is ongoing, but we caught up
    catchup_or_session_end_time = min(composition_start_time + produced_at_composition_start_time / closing_speed, session_end_time)

    if session_end_time == catchup_or_session_end_time:
        composition_duration = catchup_or_session_end_time - composition_start_time
        composed_amount = consuming_speed * composition_duration
        total_amount = session_duration * producing_speed
        remaining_amount = total_amount - composed_amount
        return catchup_or_session_end_time + remaining_amount / consuming_speed
    else:
        composition_duration = catchup_or_session_end_time - composition_start_time
        composed_amount = consuming_speed * composition_duration
        total_amount = session_duration * producing_speed
        remaining_amount = total_amount - composed_amount
        return catchup_or_session_end_time + remaining_amount / producing_speed
