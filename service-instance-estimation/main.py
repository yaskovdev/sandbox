from datetime import datetime
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


class SessionEnd(Event):

    def __init__(self, time):
        super().__init__(time, EventType.SESSION_END)


class CompositionStart(Event):

    def __init__(self, time):
        super().__init__(time, EventType.COMPOSITION_START)


class CompositionEnd(Event):

    def __init__(self, time):
        super().__init__(time, EventType.COMPOSITION_END)


faster_than_real_time_coefficient = 2

sessions = [
    (datetime(2024, 1, 1, 9, 0), datetime(2024, 1, 1, 9, 7)),
    (datetime(2024, 1, 1, 9, 3), datetime(2024, 1, 1, 9, 10)),
    (datetime(2024, 1, 1, 9, 9), datetime(2024, 1, 1, 9, 17)),
    (datetime(2024, 1, 1, 9, 6), datetime(2024, 1, 1, 9, 19)),
]

timeline = []
composition_queue = Queue()
available_composers = 2

delays = []

for session_start_time, session_end_time in sessions:
    heappush(timeline, SessionStart(session_start_time, session_end_time))
    heappush(timeline, SessionEnd(session_end_time))

while len(timeline):
    event = heappop(timeline)
    if event.event_type == EventType.SESSION_START:
        composition_queue.put((event.time, event.session_end_time))
    elif event.event_type == EventType.COMPOSITION_END:
        print(event)
        available_composers += 1

    # start composition if possible
    while not composition_queue.empty() and available_composers:
        available_composers -= 1
        session_start_time, session_end_time = composition_queue.get()
        composition_end_time = calculate_composition_end_time(faster_than_real_time_coefficient, session_start_time, session_end_time, event.time)
        heappush(timeline, CompositionEnd(composition_end_time))
        delay = composition_end_time - session_end_time
        delays.append(delay)

print(delays)
print(available_composers)
