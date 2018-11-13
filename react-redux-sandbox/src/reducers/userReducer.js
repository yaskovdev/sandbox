import {LOGIN} from '../actions/userActions'

export default function (state = {user: {loggedIn: false}}, action) {
    if (action.type === LOGIN) {
        const user = {...state.user, loggedIn: true}
        return {...state, user}
    } else {
        return state
    }
}
