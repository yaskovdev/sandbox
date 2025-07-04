from datetime import datetime
from unittest import TestCase

from util import calculate_composition_end_time


class TestUtil(TestCase):

    def test_composition_started_on_session_end(self):
        session_start_time = self._time(0)
        session_end_time = self._time(8)
        composition_start_time = self._time(8)
        composition_end_time = calculate_composition_end_time(2, session_start_time, session_end_time, composition_start_time)
        self.assertEqual(self._time(12), composition_end_time)

    def test_composition_started_after_session_end(self):
        session_start_time = self._time(0)
        session_end_time = self._time(8)
        composition_start_time = self._time(10)
        composition_end_time = calculate_composition_end_time(2, session_start_time, session_end_time, composition_start_time)
        self.assertEqual(self._time(14), composition_end_time)

    def test_composition_started_during_session_close_to_end(self):
        session_start_time = self._time(0)
        session_end_time = self._time(8)
        composition_start_time = self._time(6)
        composition_end_time = calculate_composition_end_time(2, session_start_time, session_end_time, composition_start_time)
        self.assertEqual(self._time(10), composition_end_time)

    def test_composition_started_during_session_close_to_beginning(self):
        session_start_time = self._time(0)
        session_end_time = self._time(8)
        composition_start_time = self._time(2)
        composition_end_time = calculate_composition_end_time(2, session_start_time, session_end_time, composition_start_time)
        self.assertEqual(self._time(8), composition_end_time)

    def test_composition_started_on_session_start(self):
        session_start_time = self._time(0)
        session_end_time = self._time(8)
        composition_end_time = calculate_composition_end_time(2, session_start_time, session_end_time, session_start_time)
        self.assertEqual(session_end_time, composition_end_time)

    @staticmethod
    def _time(minute):
        return datetime(2024, 1, 1, 9, minute)
