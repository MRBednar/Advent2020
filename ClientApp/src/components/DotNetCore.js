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
                        <option value="2">2</option>
                        <option value="3">3</option>
                        <option value="4">4</option>
                        <option value="5">5</option>
                        <option value="6">6</option>
                        <option value="7">7</option>
                        <option value="8">8</option>
                        <option value="9">9</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                        <option value="13">13</option>
                        <option value="14">14</option>
                        <option value="15">15</option>
                        <option value="16">16</option>
                        <option value="17">17</option>
                        <option value="18">18</option>
                        <option value="19">19</option>
                        <option value="20">20</option>
                        <option value="21">21</option>
                        <option value="22">22</option>
                        <option value="23">23</option>
                        <option value="24">24</option>
                        <option value="25">25</option>
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