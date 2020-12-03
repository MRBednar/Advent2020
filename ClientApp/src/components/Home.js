import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>Hello, world!</h1>
            <p>This is a webpage to run my Advent of Code 2020 solutions.</p>
            <p>To run the code, pick a language page, then use the dropdown to select the day</p>
      </div>
    );
  }
}
