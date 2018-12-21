import React from 'react'
import './App.css'
import {bindActionCreators} from "redux"
import {connect} from "react-redux"
import {loginAction} from "./actions/userActions"

const App = (props) => {
    const {user, login} = props
    return (
        <div className="App">
            Logged in: {user.loggedIn ? 'true' : 'false'}
            <input type={'button'} onClick={() => login(user)} value={'Login'}/>
        </div>
    )
}

const mapStateToProps = state => ({
    user: state.user.user
})

const mapDispatchToProps = dispatch => bindActionCreators({
    login: user => dispatch => {
		const value = null
		if (value.exists) {
			console.log('value exists')
		}
        dispatch(loginAction())
        console.log('user.loggedIn after loginAction dispatching is', user.loggedIn)
    }
}, dispatch)

export default connect(mapStateToProps, mapDispatchToProps)(App)
