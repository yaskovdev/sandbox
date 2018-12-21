import React, {Component} from 'react';
import {StyleSheet, TextInput, View} from 'react-native';

type Props = {};
export default class App extends Component<Props> {

    state = {value: 'Initial very long text, longer than the width of the input itself, definitely'}

    render() {
        return (
            <View style={styles.container}>
                <TextInput value={this.state.value} onChangeText={value => this.setState({value})}/>
            </View>
        );
    }
}

const styles = StyleSheet.create({
    container: {
        margin: 10,
        flex: 1,
        backgroundColor: '#fff',
        justifyContent: 'center',
    },
});
