import {SET_USER} from '../actions/userActions'

export default function (state = {users: [], id: null}, action) {
    if (action.type === SET_USER) {
        return {...state, id: action.payload}
    } else {
        return state
    }
}
