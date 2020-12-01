import React, { Component } from 'react';

export class DotNetCore extends Component {

    constructor(props) {
        super(props);
        this.state = { dayAnswer: "", loading: false, dayNumber: 1 };

        this.setAndRunDay = this.setAndRunDay.bind(this);
        this.changeDrop = this.changeDrop.bind(this);
    }

    static renderTextTest(response) {
        return (<p> {response} </p>);
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : DotNetCore.renderTextTest(this.state.dayAnswer);
        return (
            <div>
                <h1>
                    <p>Select day to run: <select value={this.state.dayNumber} onChange={this.changeDrop}>
                        <option value="1">1</option>
                    </select></p>
                    <p><button className="btn btn-primary" onClick={this.setAndRunDay}>Run for day</button></p>
                    {contents}
                </h1>
            </div>
        );
    }

    async setAndRunDay() {
        this.setState({ dayAnswer: "", loading: true, dayNumber: this.state.dayNumber }, () => { this.getDayAnswer(); });
    }

    changeDrop(event) {
        this.setState({ dayAnswer: this.state.dayAnswer, loading: this.state.loading, dayNumber: event.target.value });
    }

    async getDayAnswer() {
        var response = await fetch('dotnetday/' + this.state.dayNumber);
        var data = await response.json();
        this.setState({ dayAnswer: data, loading: false, dayNumber: this.state.dayNumber });
    }
}