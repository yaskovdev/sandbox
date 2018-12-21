import React, {PureComponent} from 'react'
import './App.css'
import {bindActionCreators} from "redux"
import {connect} from "react-redux"
import {setUserAction} from "./actions/userActions"

class App extends PureComponent {
    render() {
        console.log('render called')
        const {users, setUser} = this.props
        return (
            <div className="App">
                Number of users is: {users.length}
                <input type={'button'} onClick={() => setUser(137)} value={'Set User'}/>
            </div>
        )
    }
}

const mapStateToProps = state => {
    const active = state.user.users.filter(u => u.active)
    return ({
        users: active
    });
}

const mapDispatchToProps = dispatch => bindActionCreators({
    setUser: id => dispatch => {
        dispatch(setUserAction(id))
    }
}, dispatch)

export default connect(mapStateToProps, mapDispatchToProps)(App)
