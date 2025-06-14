from datetime import timedelta


def calculate_composition_end_time(faster_than_real_time_coefficient, session_start_time, session_end_time, composition_start_time):
    session_duration = session_end_time - session_start_time

    # composition started after session ended
    if composition_start_time >= session_end_time:
        return composition_start_time + session_duration / faster_than_real_time_coefficient

    # composition started before session ended
    closing_speed = faster_than_real_time_coefficient - 1  # real time speed is always 1
    catchup_duration = min((composition_start_time - session_start_time) / closing_speed, session_end_time - composition_start_time)

    # at this moment either the session is ended, or it is ongoing, but we caught up
    catchup_time = composition_start_time + catchup_duration

    if session_end_time > catchup_time:
        # we caught up before session ended
        return session_end_time
    else:
        # session_end_time == catchup_time, i.e., session ended
        duration_to_process_after_catchup = catchup_time - session_end_time
        duration_to_process_after_catchup = session_duration - faster_than_real_time_coefficient * catchup_duration
        return composition_start_time + catchup_duration + duration_to_process_after_catchup
