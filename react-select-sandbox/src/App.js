import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import Select from 'react-select';
import 'react-select/dist/react-select.css'

class App extends Component {

	state = { value: '' };

	handleChange = (value) => this.setState({ value });

	render() {
		const options =
			[
				{
					value: 'foo', label: 'Foo'
				},
				{
					value: 'bar', label: 'Bar'
				},
				{
					value: 'baz', label: 'Baz'
				}
			];

		return (
			<div className="App" style={{ maxWidth: '20%' }}>
				<header className="App-header">
					<img src={logo} className="App-logo" alt="logo" />
					<h1 className="App-title">Welcome to React</h1>
				</header>
				<p className="App-intro">
					To get started, edit <code>src/App.js</code> and save to reload.
				</p>
				<Select
					value={this.state.value}
					name="filter__statistics"
					options={options}
					onChange={this.handleChange}
					multi={true}
				/>
			</div>
		);
	}
}

export default App;
