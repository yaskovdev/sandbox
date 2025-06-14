from enum import Enum
from functools import total_ordering
from heapq import heappush, heappop
from queue import Queue

from util import calculate_composition_end_time


class EventType(Enum):
    SESSION_START = 0
    SESSION_END = 1
    COMPOSITION_START = 2
    COMPOSITION_END = 3


@total_ordering
class Event:

    def __init__(self, time, event_type):
        self.time = time
        self.event_type = event_type

    def __lt__(self, other):
        if self.time == other.time:
            return self.event_type.value < other.event_type.value
        else:
            return self.time < other.time

    def __eq__(self, other):
        return self.time == other.time and self.event_type == other.event_type

    def __repr__(self):
        return str(self.event_type) + " " + str(self.time)


class SessionStart(Event):

    def __init__(self, session_start_time, session_end_time):
        super().__init__(session_start_time, EventType.SESSION_START)
        self.session_end_time = session_end_time


class CompositionEnd(Event):

    def __init__(self, time):
        super().__init__(time, EventType.COMPOSITION_END)


def calculate_delays_s(sessions, total_composers, composition_speed):
    available_composers = total_composers
    timeline = []
    composition_queue = Queue()

    delays = []

    for session_start_time, session_end_time in sessions:
        heappush(timeline, SessionStart(session_start_time, session_end_time))

    while len(timeline) > 0:
        event = heappop(timeline)
        if event.event_type == EventType.SESSION_START:
            composition_queue.put((event.time, event.session_end_time))
        elif event.event_type == EventType.COMPOSITION_END:
            available_composers += 1

        # start composition if possible
        while not composition_queue.empty() and available_composers > 0:
            available_composers -= 1
            session_start_time, session_end_time = composition_queue.get()
            composition_end_time = calculate_composition_end_time(composition_speed, session_start_time, session_end_time, event.time)
            heappush(timeline, CompositionEnd(composition_end_time))
            delay = composition_end_time - session_end_time
            delays.append(delay.total_seconds())

    assert available_composers == total_composers, f"Expected {total_composers} available composers at the end of the simulation, got {available_composers}"

    return delays
