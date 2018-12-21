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

window.onerror = function (message, file, line, column, error) {
	const stack = error && error.stack ? error.stack : null;
	console.log('stack trace is', stack);
}

ReactDOM.render(<Provider store={store}><App/></Provider>, document.getElementById('root'))
registerServiceWorker()
