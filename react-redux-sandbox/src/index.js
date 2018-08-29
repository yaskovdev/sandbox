import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import {Provider} from 'react-redux'
import rootReducer from './reducers'
import thunkMiddleware from 'redux-thunk'
import {applyMiddleware, createStore} from 'redux'

const store = createStore(rootReducer, applyMiddleware(thunkMiddleware))

ReactDOM.render(<Provider store={store}><App/></Provider>, document.getElementById('root'))
registerServiceWorker()
