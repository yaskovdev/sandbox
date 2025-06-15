from datetime import datetime
from unittest import TestCase

from main import calculate_delays_s


class TestMain(TestCase):

    def test_one_session_one_real_time_instance(self):
        sessions = [
            (self._time(0), self._time(4))
        ]
        delays_s = calculate_delays_s(sessions, 1, 1)
        self.assertCountEqual([0], delays_s)

    def test_two_parallel_sessions_one_faster_than_real_time_instance(self):
        sessions = [
            (self._time(0), self._time(4)),
            (self._time(0), self._time(4))
        ]
        delays_s = calculate_delays_s(sessions, 1, 2)
        self.assertCountEqual([0, 120], delays_s)

    @staticmethod
    def _time(minute):
        return datetime(2024, 1, 1, 9, minute)
